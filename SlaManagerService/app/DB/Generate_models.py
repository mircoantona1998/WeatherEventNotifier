# -*- coding: utf-8 -*-
from sqlalchemy import create_engine, MetaData, Table, Column, text
from sqlalchemy.sql.schema import DefaultClause

# Connessione al database
connection_string = 'mssql+pyodbc://sa:RootRoot.1@127.0.0.1,1434/SLAManager?driver=ODBC+Driver+17+for+SQL+Server'
engine = create_engine(connection_string)

# Riflessione dello schema del database
original_metadata = MetaData(bind=engine)
original_metadata.reflect()

# Escludi le colonne con attributo Identity
excluded_columns = set()

for table_name, table in original_metadata.tables.items():
    for column in table.columns:
        # Verifica se la colonna è di tipo Identity
        if column.primary_key and column.autoincrement:
            excluded_columns.add((table_name, column.name))

# Creare il contenuto del file models.py
models_content = ""

for table_name, table in original_metadata.tables.items():
    models_content += f"class {table_name.capitalize()}(Base):\n"
    models_content += f'    __tablename__ = "{table_name}"\n\n'

    for col in table.columns:
        # Clona manualmente le colonne desiderate
        if (table_name, col.name) not in excluded_columns:
            server_default = col.server_default
            if server_default and isinstance(server_default.arg, DefaultClause):
                server_default = text(str(server_default.arg))
            models_content += f'    {col.name} = Column({col.type}, nullable={col.nullable}, server_default={server_default}, primary_key={col.primary_key}, unique={col.unique}, index={col.index})\n'

    models_content += "\n\n"

# Scrivi il contenuto nel file models.py
with open("models.py", "w") as file:
    file.write(models_content)

print("File models.py creato con successo.")

