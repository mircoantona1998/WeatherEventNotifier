FROM python:3.9
WORKDIR /app
COPY . .

# Install dependencies and MS SQL Server ODBC driver
RUN apt-get update \
    && apt-get install -y unixodbc-dev \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y msodbcsql17

# Install Python dependencies
RUN pip install --no-cache-dir -r requirements.txt

CMD ["python", "SlaManagerService.py"]
