"# CEAS-backend" 

# Docker

## BackEnd 

- docker build -t ceas.backend -f backend.Dockerfile .
- docker run -p 8080:80 -d ceas.backend

## FrontEnd

- docker build -t ceas.frontend -f frontend.Dockerfile .
- docker run -p 8082:80 -d ceas.frontend

NOTA: si no cargan los estilos, asegurarse de eliminar las carpetas BIN y OBJ de /FrontEnd.


# K8s

## FrontEnd

- kubectl apply -f frontend-deployment.yaml
- kubectl apply -f frontend-service.yaml
- kubectl get service frontend-service

## BackEnd 

- kubectl apply -f backend-deployment.yaml
- kubectl apply -f backend-service.yaml
- kubectl get service backend-service


## Ports
- kubectl get pods 
- kubectl port-forward pod/ <IDpod-Front> 8082:80
- kubectl port-forward pod/ <IDpod-Back> 8080:80




- gcloud auth login
- gcloud config set project ceas-project
- gcloud auth configure-docker us-central1-docker.pkg.dev
- docker build -t us-central1-docker.pkg.dev/ceas-project/ceas-repo/frontend:latest -f frontend.Dockerfile .
- docker build -t us-central1-docker.pkg.dev/ceas-project/ceas-repo/backend:latest -f backend.Dockerfile .
- docker push us-central1-docker.pkg.dev/ceas-project/ceas-repo/frontend:latest
- docker push us-central1-docker.pkg.dev/ceas-project/ceas-repo/backend:latest
- gcloud container clusters get-credentials ceas-gke --region us-central1 --project ceas-project
- kubectl config current-context (ver proyecto actual)
- kubectl apply -f frontend-deployment.yaml
- kubectl apply -f backend-deployment.yaml
- kubectl get deployments
- kubectl get pods
- kubectl apply -f frontend-service.yaml
- kubectl apply -f backend-service.yaml
- kubectl get services
- kubectl proxy --port 8080