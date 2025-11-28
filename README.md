# 🚀 ASISYA API (.NET Core 8)

## 🌟 Introducción

Este proyecto es una **API RESTful** desarrollada en **.NET Core 8** (C#) con un enfoque en la arquitectura por capas, diseñada para gestionar la lógica de negocio relacionada con **Productos y Categorías**.

Utiliza **SQL Server Express** como motor de base de datos y un robusto sistema de seguridad basado en **JSON Web Tokens (JWT)** para proteger los *endpoints*.

## 🏛️ Arquitectura de la Solución (Capas)

La solución está organizada en un patrón de capas para promover la **Separación de Responsabilidades** (SoC), la mantenibilidad y la escalabilidad.

| Capa/Proyecto | Responsabilidad Principal | Descripción |
| :--- | :--- | :--- |
| **ASISYA** (API Principal) | Presentación / Lógica de Negocio | Contiene los **Controladores** (Lógica de Negocio, Seguridad y *Endpoints*). Es el punto de entrada de todas las peticiones. |
| **FACADE** | Patrón Fachada / Mediador | Actúa como un **puente** o mediador entre los Controladores (lógica de negocio) y los Dacs (acceso a datos). |
| **DACS** | Acceso a Datos (CRUD) | Contiene las clases `InsertarDac`, `ActualizarDac`, `EliminarDac` y `ConsultarDac`. Responsable de ejecutar consultas directas a la base de datos. |
| **ASISYADB** | Modelos de Datos (DTOs) | Contiene los **DTOs** (Data Transfer Objects) utilizados para el intercambio de datos entre capas y la exposición al cliente, en lugar de utilizar las tablas directas. |

## ⚙️ Requisitos del Sistema

Para poder compilar y ejecutar este proyecto localmente, necesitas lo siguiente:

* **SDK de .NET Core 8.0** o superior.
* **Visual Studio 2022** o un IDE compatible.
* **SQL Server Express** o SQL Server para la base de datos.

---

## 🔑 Configuración y Seguridad

### 1. Base de Datos

La cadena de conexión se encuentra en el archivo **`appsettings.json`** del proyecto principal (`ASISYA`).

> **Nota:** La configuración actual está optimizada para instancias de **SQL Server Express**. Si utilizas otra instancia de SQL Server, ajusta la cadena de conexión en el `appsettings.json`.

### 2. Seguridad (JSON Web Tokens - JWT)

Todos los *endpoints* sensibles están protegidos con el atributo `[Authorize]`, lo que requiere un token JWT válido.

#### Obtener un Token

Antes de acceder a cualquier recurso, debes generar un token de seguridad:

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `POST` | `/Authentication/GenerarToken` | Genera un token JWT para autenticar las peticiones. |

#### Uso del Token

El token generado debe incluirse en la cabecera (**Header**) de cada petición protegida:

* **Key:** `Authorization`
* **Value:** `Bearer [TU_TOKEN_GENERADO]`
* **User:** `ASISYATOKEN`
* **Pass:** `Sup3rU53r*_xx`
---

## 💻 Endpoints del `ProductosController`

El `ProductosController` contiene la lógica de negocio principal. Todos estos *endpoints* requieren autenticación (el token JWT).

| Método | Endpoint | Descripción | DTOs (Data Transfer Objects) |
| :--- | :--- | :--- | :--- |
| `POST` | `/Productos/CrearCategorias` | Registra una nueva categoría de forma individual. | Recibe DTO de Categoría. |
| `POST` | `/Productos/CrearProductosMasivos` | Crea una colección de productos a la vez, recibiendo un JSON de lista. | Recibe lista de DTOs de Producto. |
| `GET` | `/Productos/ObtenerProductosFiltrados` | Recupera una lista de productos aplicando diversos filtros (e.g., por nombre, categoría, rango de precio). | Retorna lista de DTOs de Producto. |
| `GET` | `/Productos/ObtenerProductoID/{id}` | Recupera un único producto utilizando su identificador único. | Retorna un DTO de Producto. |
| `PUT` | `/Productos/ActualizarProducto` | Actualiza la información de un producto existente. | Recibe DTO de Producto con la información actualizada. |
| `DELETE`| `/Productos/EliminarProducto/{id}` | Elimina un producto de la base de datos mediante su ID único. | Recibe el `id` en el URL. |
