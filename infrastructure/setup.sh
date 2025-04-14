#!/bin/bash

kubectl apply -f subscriptions-postgres-statefulset.yaml
kubectl apply -f notifications-postgres-statefulset.yaml

kubectl apply -f subscriptions-postgres-service.yaml
kubectl apply -f notifications-postgres-service.yaml

kubectl apply -f subscriptions-api-deployment.yaml
kubectl apply -f notifications-api-deployment.yaml

kubectl apply -f subscriptions-api-service.yaml
kubectl apply -f notifications-api-service.yaml

kubectl apply -f keycloak-deployment.yaml
kubectl apply -f keycloak-service.yaml

kubectl apply -f oauth2-proxy-deployment.yaml
kubectl apply -f oauth2-proxy-service.yaml

kubectl apply -f ingress.yaml
kubectl apply -f keycloak-ingress.yaml

echo "All resources applied"
