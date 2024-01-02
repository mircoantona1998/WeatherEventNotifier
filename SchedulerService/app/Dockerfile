# Usa un'immagine di Python come base
FROM python:3.9

# Imposta la directory di lavoro all'interno del contenitore
WORKDIR /app

# Copia i file dell'applicazione nella directory di lavoro
COPY . .

# Installa le dipendenze dell'app
RUN pip install --no-cache-dir -r requirements.txt



# Definisci il comando per eseguire l'applicazione
CMD ["python", "SchedulerService.py"]
