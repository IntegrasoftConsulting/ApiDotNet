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
```

**Nota**: La interfaz de Swagger cargará automáticamente en la raíz (/) de la URL generada por Codespaces gracias a la configuración de RoutePrefix.

### 3. Ejecución de Pruebas Unitarias
Para validar la integridad de los contratos y la lógica de negocio sin depender de la infraestructura:
```bash
dotnet test
```

**Tip de Codespaces**: Puedes usar la extensión "Test Explorer" en la barra lateral para ejecutar y depurar pruebas de forma visual con un solo clic.

## 🛠️ Pilares Técnicos Implementados

### 🔹 Inversión de Control (IoC) e Inyección de Dependencias
Se utiliza el contenedor nativo de .NET 9 para gestionar el ciclo de vida de los objetos. La comunicación entre capas es **100% abstracta**; las clases de nivel superior (Application) no instancian implementaciones de nivel inferior (Infrastructure), sino que solicitan interfaces a través del constructor (*Constructor Injection*).


### 🔹 Resiliencia y Manejo Global de Errores
Implementación de un **Global Exception Handler** basado en la interfaz `IExceptionHandler`. Este componente centralizado intercepta excepciones personalizadas lanzadas desde el **Dominio** y las traduce automáticamente a respuestas HTTP estandarizadas:
* **ProductNotFoundException** ➡️ `404 Not Found`
* **DomainException** ➡️ `400 Bad Request`
* **Excepciones no controladas** ➡️ `500 Internal Server Error`

### 🔹 Domain-Driven Design (DDD) - Value Objects
Uso de `records` de C# para encapsular lógica de validación e inmutabilidad. 
* **Beneficio:** Asegura que los datos de negocio (como el objeto `Price`) sean válidos desde su creación, eliminando la "Anemia del Dominio" y garantizando que las reglas de negocio se cumplan en el núcleo del sistema.

### 🔹 Observabilidad con Serilog
Configuración de **Serilog** como motor de logging estructurado en el adaptador de entrada (API). El registro de eventos se consume mediante la abstracción estándar `ILogger<T>` en las capas internas, permitiendo que la aplicación sea agnóstica a la herramienta de monitoreo final (Console, Azure Insights, ELK).

---

## 🧪 Estrategia de Testing (xUnit + Moq)
Se implementó una suite de pruebas unitarias de caja blanca. Al moquear los "Puertos de Salida" (Interfaces de Repositorios), garantizamos que las pruebas de la lógica de negocio sean:
1. **Aisladas:** Solo prueban el comportamiento del servicio, no la base de datos.
2. **Deterministas:** El resultado no depende de factores externos o latencia de red.
3. **Velocidad:** Ejecución en milisegundos para ciclos de feedback rápidos.



---

## 📚 Hoja de Ruta (Roadmap)
- [ ] Implementar persistencia real con **Entity Framework Core** y migraciones automáticas.
- [ ] Agregar validación de esquemas con **FluentValidation** en la capa de entrada.
- [ ] Integrar **GitHub Spec Kit** para flujos de *Spec-Driven Development* (Diseño de API primero).
- [ ] Configurar **GitHub Actions** para ejecución automática de tests en cada *Pull Request*.

---
*Este proyecto representa un ecosistema de desarrollo moderno, escalable y preparado para procesos de transformación digital e integración continua.*