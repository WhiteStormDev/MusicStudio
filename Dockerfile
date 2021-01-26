# https://hub.docker.com/_/microsoft-dotnet-coreasds
# FROM gcr.io/google-appengine/aspnetcore:2.1.1
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY /MusicStudio/BLInterfaces/* ./MusicStudio/BLInterfaces/
COPY /MusicStudio/BLService/* ./MusicStudio/BLService/
COPY /MusicStudio/DB/* ./MusicStudio/DB/
COPY /MusicStudio/DBLayer/* ./MusicStudio/DBLayer/
COPY /MusicStudio/MusicStudioApplication/* ./MusicStudio/MusicStudioApplication/
COPY /MusicStudio/MusicStudioModels/* ./MusicStudio/MusicStudioModels/
COPY /MusicStudio/ServerControls/* ./MusicStudio/ServerControls/
COPY /MusicStudio/Services/* ./MusicStudio/Services/
# COPY /FrontEnd/* ./FrontEnd/
COPY /MusicStudio/MusicStudio.sln ./MusicStudio/MusicStudio.sln
# WORKDIR /source/MusicStudio

RUN dotnet restore /MusicStudio/MusicStudio.sln

# copy and publish app and libraries
# COPY . ../.
# WORKDIR /source
RUN dotnet publish -c release -o /app --no-restore /MusicStudio/MusicStudio.sln

# final stage/image s
FROM gcr.io/google-appengine/aspnetcore:2.1.1
# FROM mcr.microsoft.com/dotnet/core/runtime:2.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MusicStudio.dll"]
