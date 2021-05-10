# InfoTv
Application en Blazor server pour afficher des informations sur des téléviseurs, ou autre via des raspberry pi.
L'affichage est optimisé pour des téléviseurs en 1080p et en paysage.

## Serveur

Login d'accès aux paramètrages :
login : root
password : Azerty123!

### Login Page  
http://adressServer/Identity/Account/Login  

### Page paramètrages :  
http://adressServer/settings (il faut être authentifié)  
Permet d'injecter un PowerPoint exporté en format MP4.
Ajout d'un message qui sera affiché dans un bandeau en haut de la page Info.
- Critique : couleur rouge
- Attention : couleur orange
- Normal : pas de couleur
La date de fin s'est pour définir la fin de l'affichage du message.

### Page info
http://adressServer/info  
Utilise [SignalR](https://docs.microsoft.com/fr-fr/aspnet/signalr/overview/getting-started/introduction-to-signalr) pour que les affichages se synchronisent lors d'un changement sur la page de paramètrages.

## Configuration du Raspberry

Installation de chromium
```
sudo apt-get install chromium-browser
```

Il faut configurer le raspberry pour qu'au démarrge il ouvre Chromium sur l'adresse des informations, et en kiosk.  
```
sudo nano /etc/profile
```

Pour autostart Chromium, il faut créer un fichier `autostart` avec :
```
sudo nano ~/.config/lxsession/LXDE/autostart
```
Ajouter ces lignes :
```
# Désactive le screen saver, la gestion d'énergie et l'écran blanc
xset s off
xset s noblank
xset -dpms

@chromium-browser --disable-infobars --kiosk http://YourServerAddress/info
```
