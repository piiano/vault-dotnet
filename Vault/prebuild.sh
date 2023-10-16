#!/bin/bash

. ../.env
OPENAPI_YAML=openapi.yaml

VAULT_VERSION_STR="${VAULT_VERSION//./-}"
echo "Downloading openapi for Vault ${VAULT_VERSION}"
curl -L -s https://docs.piiano.com/v${VAULT_VERSION_STR}/assets/openapi.json --output openapi.json
