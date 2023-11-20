"# CEAS-backend" 

# Docker

## BackEnd 

- cd WebApi
- dotnet publish -c Release -o out
- cd ..
- docker build -t ceas.backend -f backend.Dockerfile .
- docker run -p 8080:80 -d ceas.backend

## FrontEnd

- cd FrontEnd
- dotnet publish -c Release -o out
- cd ..
- docker build -t ceas.frontend -f frontend.Dockerfile .
- docker run -p 8082:80 -d ceas.frontend