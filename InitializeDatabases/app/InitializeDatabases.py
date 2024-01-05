import os
import mysql.connector
import pyodbc
import time

def is_sql_server_available():
    try:
        conn = pyodbc.connect(connection_string_master)
        conn.close()
        return True
    except:
        return False
    
def is_mysql_available():
    try:
        conn = mysql.connector.connect(**mysql_config)
        conn.close()
        return True
    except :
        return False
mysql_config = {
    'user': os.getenv('MYSQL_USER', 'root'),
    'password': os.getenv('MYSQL_PASSWORD', 'root'),
    'host': os.getenv('MYSQL_HOST', 'localhost'),
    'port': int(os.getenv('MYSQL_PORT', 3307)),  
}

sql_server_host = os.getenv('SQL_SERVER_HOST', 'localhost')
sql_server_port = os.getenv('SQL_SERVER_PORT', '1433')
sql_server_database = os.getenv('SQL_SERVER_DATABASE', 'Userdata')
sql_server_user = os.getenv('SQL_SERVER_USER', 'sa')
sql_server_password = os.getenv('SQL_SERVER_PASSWORD', 'RootRoot.1')

connection_string_master = (
    f"DRIVER={{ODBC Driver 17 for SQL Server}};"
    f"SERVER={sql_server_host},{sql_server_port};"
    f"UID={sql_server_user};"
    f"PWD={sql_server_password};"
)

connection_string = (
    f"DRIVER={{ODBC Driver 17 for SQL Server}};"
    f"SERVER={sql_server_host},{sql_server_port};"
    f"DATABASE={sql_server_database};"
    f"UID={sql_server_user};"
    f"PWD={sql_server_password};"
)
while not is_sql_server_available():
    print("Attendo la disponibilita di SQL Server...")
    time.sleep(5)

while not is_mysql_available():
    print("Attendo la disponibilita di MySQL...")
    time.sleep(5)
    
dump_file_path = 'init-mysql.sql'
def read_dump(file_path):
    with open(file_path, 'r') as file:
        return file.read()
dump_script = read_dump(dump_file_path)
scripts = dump_script.split('-- Dump completed on')
dump_script = read_dump(dump_file_path)
connection = mysql.connector.connect(**mysql_config)
try:
    cursor = connection.cursor()
    commands = dump_script.split(';')
    for command in commands:
        cursor.execute(command)
    connection.commit()
    print(f"Script executed successfully for database: mysql")
except mysql.connector.Error as e:
    print(f"Error for database mysql: {e}")
finally:
    cursor.close()
    connection.close()
    
sql_server_script_path = 'init-sqlServer.sql'
with open(sql_server_script_path, 'r', encoding='utf-16-le') as file:
    sql_server_dump = file.read()
sql_server_dump = sql_server_dump.replace('\ufeff', '')
def execute_sql_command(connection_string, command):
    connection = pyodbc.connect(connection_string, autocommit=True)
    cursor = connection.cursor()
    try:
        cursor.execute(command)
        connection.commit()
        print(f"Comando eseguito con successo:\n{command}")
    except Exception as e:
        connection.rollback()
        print(f"Errore nell'esecuzione del comando:\n{command}\nErrore: {str(e)}")
    finally:
        connection.close()
sql_commands = [cmd.strip() for cmd in sql_server_dump.split("GO") if cmd.strip()]
execute_sql_command(connection_string_master, "DROP DATABASE Userdata")
execute_sql_command(connection_string_master, "CREATE DATABASE [Userdata] CONTAINMENT = NONE ON  PRIMARY ( NAME = N'Userdata', FILENAME = N'/var/opt/mssql/data/Userdata.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ) LOG ON ( NAME = N'Userdata_log', FILENAME = N'/var/opt/mssql/data/Userdata_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB ) WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF")
for command in sql_commands:
    execute_sql_command(connection_string, command)


