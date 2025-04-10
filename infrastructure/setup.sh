#!/bin/bash

kubectl apply -f subscriptions-postgres-statefulset.yaml
kubectl apply -f notifications-postgres-statefulset.yaml

kubectl apply -f subscriptions-postgres-service.yaml
kubectl apply -f notifications-postgres-service.yaml

kubectl apply -f subscriptions-api-deployment.yaml
kubectl apply -f notifications-api-deployment.yaml

kubectl apply -f subscriptions-api-service.yaml
kubectl apply -f notifications-api-service.yaml

kubectl apply -f ingress.yaml

echo "All resources applied"