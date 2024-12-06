SHELL := /bin/bash

id?=$(shell whoami)
location?=northeurope
rgName?=rg-asbus-$(id)

.PHONY: all status outputs clean

all:
	az account show
	az group create --name $(rgName) --location $(location)
	az deployment group create \
		--resource-group $(rgName) \
		--template-file main.bicep \
		--parameters location=$(location)

status:
	az resource list --resource-group $(rgName) --location $(location) --output table

outputs:
	az deployment group list --resource-group $(rgName) --query "[].properties.outputs"

lint:
	bicep lint main.bicep

clean:
	az group delete --name $(rgName)
