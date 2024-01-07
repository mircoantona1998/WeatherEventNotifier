import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart
from Utils.Logger import Logger
import inspect
from datetime import datetime
class EmailService:
    def __init__(self, smtp_server, smtp_port, smtp_username, smtp_password):
        self.smtp_server = smtp_server
        self.smtp_port = smtp_port
        self.smtp_username = smtp_username
        self.smtp_password = smtp_password

    def send_email(self, to_email, subject, message):
        try:
            msg = MIMEMultipart()
            msg['From'] = self.smtp_username
            msg['To'] = to_email
            msg['Subject'] = subject
            msg.attach(MIMEText(message, 'plain'))

            with smtplib.SMTP(self.smtp_server, self.smtp_port) as server:
                server.starttls()
                server.login(self.smtp_username, self.smtp_password)
                server.sendmail(self.smtp_username, to_email, msg.as_string())
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - send_email - {inspect.currentframe().f_globals['__file__']}")
            return True
        except Exception as e:
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - send_email ERROR- {inspect.currentframe().f_globals['__file__']}")
            return False