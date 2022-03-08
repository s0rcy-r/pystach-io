# syntax=docker/dockerfile:1

#Récupération de l'image de dotnet core 3.1
FROM mcr.microsoft.com/dotnet/aspnet:3.1

#Copie des fichiers de publications au sein du container
COPY /deployment_files/app App/

#Changement du dossier de travail
WORKDIR /App

#Lancement de la dll de l'application lors du démarrage du container
ENTRYPOINT ["dotnet", "pystach-io.dll"]