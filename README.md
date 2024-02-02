# WeatherEventNotifier
ABSTRACT

L’elaborato è pensato per offrire un servizio agli utenti di avviso mediante Mail, Telegram e/o notifica push su un applicazione mobile. 

L’ utente una volta fatto accesso all’applicazione mediante autenticazione username-password, può creare e gestire delle configurazioni. 

Una configurazione è formata da:
-	Latitudine della zona interessata
-	Longitudine della zona interessata
-	Metrica (temperatura massima, temperatura minima, probabilità di pioggia, ecc..) in accordo con quelle di OpenWeather
-	Frequenza che indica ogni quanto l’applicazione deve fare verifica della condizione (ogni ora, ogni 2 ore, ogni 4 ore, ogni 8 ore, una volta al giorno)
-	Valore per cui deve scattare la notifica
-	Simbolo, (<,>,<=,>=,==) che serve per confrontare la misura effettiva della metrica scelta con il valore di soglia

L’utente può scegliere di disattivare/attivare i canali su cui vuole ricevere le informazioni che sono 3:
-	Telegram (passando un id che rappresenta il proprio chat_id)
-	Mail (configurando nel proprio profilo la propria mail)
-	Notifiche push (che riceverà all’interno dell’applicazione mobile nella sezione notifiche)
