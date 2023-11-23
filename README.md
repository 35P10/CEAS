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


# K8s

## BackEnd 

- kubectl apply -f frontend-deployment.yaml
- kubectl apply -f frontend-service.yaml
- kubectl get service frontend-service

## FrontEnd

- kubectl apply -f backend-deployment.yaml
- kubectl apply -f backend-service.yaml
- kubectl get service backend-service

- kubectl get svc frontend-service



