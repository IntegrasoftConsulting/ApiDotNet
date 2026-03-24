# 🛡️ Hexagonal Architecture & Domain-Driven Design (DDD) - .NET 9

Este repositorio contiene una implementación de referencia de **Arquitectura Hexagonal** (también conocida como *Ports and Adapters*) desarrollada en **.NET 9**. El proyecto está diseñado para ser ejecutado íntegramente en **GitHub Codespaces**, proporcionando un entorno de desarrollo en la nube preconfigurado y estandarizado.

---

## 🏗️ Estructura de Capas (Layers)

La solución sigue el principio de **Dependencia hacia el Centro**, donde el núcleo (Dominio) es independiente de cualquier tecnología externa.

| Capa | Responsabilidad | Dependencias |
| :--- | :--- | :--- |
| **Domain** | **El Núcleo.** Contiene Entidades, **Value Objects**, Excepciones de Dominio e interfaces de salida (Output Ports). | Ninguna |
| **Application** | **Casos de Uso.** Orquesta la lógica del negocio. Contiene las interfaces de entrada (Input Ports) y servicios. | Domain |
| **Infrastructure** | **Adaptadores de Salida.** Implementaciones técnicas: Repositorios (MySQL), Clientes API, Mensajería. | Domain |
| **API** | **Adaptador de Entrada.** Punto de entrada HTTP, configuración de IoC, Swagger y Middleware de excepciones. | Application, Infrastructure |
| **UnitTests** | **Validación.** Pruebas de caja blanca sobre la lógica de aplicación utilizando **Mocks**. | Application, Domain |

---

## 🚀 Guía de Inicio Rápido en Codespaces

### 1. Requisitos
Al abrir este repositorio en **GitHub Codespaces**, el entorno utiliza el archivo `.devcontainer/devcontainer.json` para instalar:
* .NET 9 SDK
* C# Dev Kit & IntelliCode
* Soporte para OpenAPI/Swagger

### 2. Ejecución de la API
Para levantar el servicio y acceder a la documentación interactiva:
```bash
dotnet run --project src/Hexagonal.API/Hexagonal.API.csproj