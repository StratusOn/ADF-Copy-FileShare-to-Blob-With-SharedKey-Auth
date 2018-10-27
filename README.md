# Using SharedKey Auth in an ADF Pipeline

This simple ADF pipeline demonstrates copying files from an Azure Files share to an Azure Blob container, followed by archiving the source files and then deleting what has been successfully copied.

The pipeline leverages the Copy activity to perform the copying and archiving. In order for it to perform the deletion, a REST call is made to Azure Files with a shared key signature in the Authorization header. The signature is generated in the pipeline with the only external call made to an Azure Function to perform an HMAC SHA-256 encoding call.

## Deploying
The sample pipeline can be deployed via an ARM template in less than 2 minutes:

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FStratusOn%2FADF-Blob-SharedKey-Auth%2Fmaster%2Fsrc%2Fazuredeploy.json)
