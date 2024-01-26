import json
from datetime import datetime
from Utils.Json import Json
from Utils.Logger import Logger
import inspect
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from datetime import datetime, timedelta
from DB.Repository.SlaMetricViolationRepo import SlaMetricViolationRepo
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from datetime import datetime, timedelta
from sklearn.metrics import accuracy_score, classification_report
from sklearn.model_selection import cross_val_score
class EventHandlers:
    
    #FORECAST
    def handle_tag_GetForecast( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetConfiguration  - {inspect.currentframe().f_globals['__file__']}")

        # Get violations data
        violations = SlaMetricViolationRepo.get_all_by_idSla_minutes(data["IdSla"], data["Minutes"])

        if len(violations) == 0:
            return "0%"

        # Extract violation datetimes
        datetime_viol = [pd.to_datetime(violation["Datetime"]) for violation in violations]

        # Create a DataFrame with datetime and violation columns
        current_time = datetime.utcnow().replace(second=0, microsecond=0)
        datetime_list = [current_time - timedelta(minutes=x) for x in range(data["Minutes"] + 1)]

        transformed_data = {
            'Datetime': datetime_list,
            'Violation': [1 if dt in datetime_viol else 0 for dt in datetime_list]
        }

        df = pd.DataFrame(transformed_data)
        df['Datetime'] = pd.to_datetime(df['Datetime'])

        # Feature engineering: add a column for the previous violation
        df['PreviousViolation'] = df['Violation'].shift(1)

        # Target variable
        df['Target'] = df['Violation']

        # Drop rows with NaN values introduced by shift
        df_clean = df.dropna()

        # Features and target
        X = df_clean[['Datetime', 'PreviousViolation']]
        y = df_clean['Target'].astype(int)

        X['Datetime'] = X['Datetime'].astype('int64')

        try:
            # Split data into training and testing sets
            test_size = 0.2
            X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=test_size, random_state=42, stratify=y)

            # Train a RandomForestClassifier
            model = RandomForestClassifier(random_state=42)
            model.fit(X_train, y_train)

            # Evaluate the model
            y_pred = model.predict(X_test)
            accuracy = accuracy_score(y_test, y_pred)
            print(f"Accuracy: {accuracy:.2%}")
            print("Classification Report:")
            print(classification_report(y_test, y_pred))

            # Cross-validate the model
            cv_scores = cross_val_score(model, X, y, cv=5, scoring='accuracy')
            print(f"Cross-validated Accuracy: {cv_scores.mean():.2%} (std: {cv_scores.std():.2%})")

            # Make predictions only if there is at least one sample in the testing set
            if len(X_test) > 0:
                # Make a prediction for the current time
                current_datetime = datetime.utcnow()
                current_features = pd.DataFrame([[current_datetime, df['Violation'].iloc[-1]]], columns=['Datetime', 'PreviousViolation'])
                # Convert Datetime to numeric for the prediction
                current_features['Datetime'] = current_features['Datetime'].astype('int64')
                prediction = model.predict(current_features)[0]
                probability = model.predict_proba(current_features)[0][1]

                print(f"Current DateTime: {current_datetime}")
                print(f"Prediction: {prediction}")
                print(f"Probability of Violation in the next {data['Minutes']} minutes: {probability:.2%}")

                return f"{probability:.2%}"
            else:
                print("Not enough samples in the testing set.")
                return "Not enough samples in the testing set."
        except Exception as e:
            # Handle exceptions, such as when there's only one class in the target variable
            print(f"Error: {e}")
            print("Not enough classes in the target variable.")
            # Use all data for training
            X_train, y_train = X, y
            model = RandomForestClassifier(random_state=42)
            model.fit(X_train, y_train)

            # Make a prediction for the current time
            current_datetime = datetime.utcnow()
            current_features = pd.DataFrame([[current_datetime, df['Violation'].iloc[-1]]], columns=['Datetime', 'PreviousViolation'])
            # Convert Datetime to numeric for the prediction
            current_features['Datetime'] = current_features['Datetime'].astype('int64')
            prediction = model.predict(current_features)[0]
            probability = model.predict_proba(current_features)[0][1]

            print(f"Current DateTime: {current_datetime}")
            print(f"Prediction: {prediction}")
            print(f"Probability of Violation in the next {data['Minutes']} minutes: {probability:.2%}")

            return f"{probability:.2%}"


    tag_handlers = {
    "GetForecast": handle_tag_GetForecast,
    }
    




