# Using SharedKey Auth in an ADF Pipeline

This simple ADF pipeline demonstrates copying files from an Azure Files share to an Azure Blob container, followed by archiving the source files and then deleting what has been successfully copied.

The pipeline leverages the Copy activity to perform the copying and archiving. In order for it to perform the deletion, a REST call is made to Azure Files with a shared key signature in the Authorization header. The signature is generated in the pipeline with the only external call made to an Azure Function to perform an HMAC SHA-256 encoding call.
