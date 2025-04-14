#!/bin/bash

kubectl delete -f subscriptions-postgres-statefulset.yaml
kubectl delete -f notifications-postgres-statefulset.yaml

kubectl delete -f subscriptions-postgres-service.yaml
kubectl delete -f notifications-postgres-service.yaml

kubectl delete -f subscriptions-api-deployment.yaml
kubectl delete -f notifications-api-deployment.yaml

kubectl delete -f subscriptions-api-service.yaml
kubectl delete -f notifications-api-service.yaml

kubectl delete -f keycloak-deployment.yaml
kubectl delete -f keycloak-service.yaml

kubectl delete -f oauth2-proxy-deployment.yaml
kubectl delete -f oauth2-proxy-service.yaml

kubectl delete -f ingress.yaml
kubectl delete -f keycloak-ingress.yaml

echo "All resources deleted"
