#!/bin/bash

# Variables
RESOURCE_GROUP="AZDocVault"
LOCATION="centralindia"
STORAGE_ACCOUNT="dvstorage1818"
COSMOS_ACCOUNT="myaz-cosmos-account"
DATABASE_NAME="docvaultdb"
CONTAINER_NAME="documents"

echo "Creating Resource Group..."
az group create \
  --name $RESOURCE_GROUP \
  --location $LOCATION

echo "Creating Storage Account..."
az storage account create \
  --name $STORAGE_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION \
  --sku Standard_LRS

echo "Creating Cosmos DB Account..."
az cosmosdb create \
  --name $COSMOS_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --locations regionName=$LOCATION failoverPriority=0 \
  --default-consistency-level Session

echo "Creating Cosmos Database..."
az cosmosdb sql database create \
  --account-name $COSMOS_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --name $DATABASE_NAME

echo "Creating Cosmos Container..."
az cosmosdb sql container create \
  --account-name $COSMOS_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --database-name $DATABASE_NAME \
  --name $CONTAINER_NAME \
  --partition-key-path "/userId" \
  --throughput 400

echo "Infrastructure setup complete!"
