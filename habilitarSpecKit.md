# Instalar el validador de Spectral (para asegurar que tu Swagger sigue buenas prácticas)
npm install -g @stoplight/spectral-cli

# Instalar Microsoft Kiota (Herramienta de generación de clientes basada en Spec)
dotnet tool install --global Microsoft.OpenApi.Kiota


"customizations": {
  "vscode": {
    "extensions": [
      "42Crunch.vscode-openapi", // Editor visual y seguridad de Swagger
      "stoplight.spectral",      // Linter de especificaciones
      "ms-azuretools.vscode-docker" 
    ]
  }
}


https://github.com/siri404/devcontainer-ai-features