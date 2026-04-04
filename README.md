# TALLERBD2026 — Sistema de Gestión SNet

Aplicación de escritorio desarrollada en **C# con Windows Forms** para la gestión de clientes, proveedores y localidades geográficas de Chile. El proyecto fue construido sobre una arquitectura en **4 capas** utilizando **SQL Server** como motor de base de datos.

---

## 📁 Estructura del Proyecto

```
Aiep2025.sln
├── Entidad/          ← Modelos / Clases de dominio (DTOs)
├── Datos/            ← Acceso a datos (ADO.NET + Stored Procedures)
├── Negocio/          ← Lógica de negocio (capa intermedia)
└── Presentacion/     ← Interfaz gráfica (Windows Forms)
```

---

## 🏗️ Arquitectura en Capas

### 1. `Entidad` — Capa de Modelos

Contiene las clases que representan las tablas de la base de datos. Cada clase expone sus campos como propiedades con getter y setter.

| Clase        | Descripción                                          |
|--------------|------------------------------------------------------|
| `ECliente`   | Datos de un cliente (RUT, nombre, dirección, giro…)  |
| `EProv`      | Datos de un proveedor                                |
| `EUsua`      | Datos de un usuario del sistema                      |
| `ELogin`     | Credenciales de inicio de sesión                     |
| `ELocReg`    | Región geográfica                                    |
| `ELocPro`    | Provincia geográfica                                 |
| `ELocCom`    | Comuna geográfica                                    |
| `Respuesta<T>` | Wrapper genérico para retornar estado + mensaje + objeto desde cualquier operación |

### 2. `Datos` — Capa de Acceso a Datos

Se comunica directamente con SQL Server mediante **ADO.NET** (`SqlConnection`, `SqlCommand`, `SqlDataReader`). Todas las operaciones de base de datos invocan **Stored Procedures**. Cada clase implementa el patrón **Singleton**.

| Clase       | Stored Procedures utilizados                                        |
|-------------|---------------------------------------------------------------------|
| `DCliente`  | `Bus_Cliente`, `Bus_Rut_Cliente`, `Ing_Cliente`, `Act_Cliente`, `Eli_Cliente`, `Ult_Cliente` |
| `DProv`     | Operaciones CRUD para proveedores                                   |
| `DLogin`    | `IngSig` (autenticación por nombre y contraseña)                    |
| `DUsua`     | Gestión de usuarios del sistema                                     |
| `DLocReg`   | Consulta de regiones                                                |
| `DLocPro`   | Consulta de provincias                                              |
| `DLocCom`   | Consulta de comunas                                                 |
| `Conexion`  | Centraliza la cadena de conexión a SQL Server                       |
| `Parametro` | Clase auxiliar para parámetros reutilizables                        |

**Cadena de conexión por defecto (LocalDB):**
```
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SNet;Integrated Security=True
```

> También existe una cadena alternativa comentada para entornos AIEP con autenticación SQL.

### 3. `Negocio` — Capa de Lógica de Negocio

Actúa como puente entre la presentación y los datos. No accede directamente a la base de datos; delega las operaciones a la capa `Datos` y retorna objetos `Respuesta<T>` a la capa de presentación.

| Clase       | Responsabilidad                              |
|-------------|----------------------------------------------|
| `NCliente`  | Orquesta el CRUD de clientes                 |
| `NProv`     | Orquesta el CRUD de proveedores              |
| `NLogin`    | Valida credenciales de inicio de sesión      |
| `NUsua`     | Gestión de usuarios                          |
| `NLocReg`   | Listado de regiones                          |
| `NLocPro`   | Listado de provincias                        |
| `NLocCom`   | Listado de comunas                           |

### 4. `Presentacion` — Capa de Interfaz Gráfica

Aplicación **Windows Forms** (.NET Framework). Contiene el formulario principal (`Menu`) que actúa como shell MDI, cargando los formularios hijos dentro de un panel central.

```
Presentacion/
├── Program.cs              ← Punto de entrada (Application.Run)
├── Menu.cs / .Designer.cs  ← Formulario principal con menú de navegación
├── Configuracion.cs        ← Almacena el usuario activo en sesión (EUsua)
├── Cliente/
│   ├── PCli_Ing.*          ← Ingreso de clientes
│   ├── PCli_Con.*          ← Consulta/listado de clientes (con botones Modificar/Eliminar)
│   └── PCli_Act.*          ← Actualización de clientes
├── Proveedor/
│   ├── PProv_Ing.*         ← Ingreso de proveedores
│   ├── PProv_Con.*         ← Consulta/listado de proveedores
│   └── PProv_Act.*         ← Actualización de proveedores
├── Localidad/
│   ├── PReg.*              ← Gestión de Regiones
│   ├── PPro.*              ← Gestión de Provincias
│   └── PCom.*              ← Gestión de Comunas
└── AAClases/
    ├── ValidaRut.cs        ← Validación y formateo de RUT chileno (algoritmo mod 11)
    └── Filtrar.cs          ← Utilidad para filtrar datos en grillas
```

---

## 🔑 Tecnologías Clave

| Tecnología            | Uso                                          |
|-----------------------|----------------------------------------------|
| C# (.NET Framework)   | Lenguaje y plataforma principal              |
| Windows Forms (WinForms) | Interfaz gráfica de usuario               |
| ADO.NET               | Acceso a base de datos (sin ORM)            |
| SQL Server / LocalDB  | Motor de base de datos relacional            |
| Stored Procedures     | Toda la lógica SQL reside en el servidor     |
| Visual Studio 2022    | IDE de desarrollo                            |

---

## 🧩 Patrones de Diseño Utilizados

- **Singleton**: Cada clase de la capa `Datos` expone una única instancia estática (`Instancia`) para evitar múltiples conexiones concurrentes.
- **Wrapper / Response Object**: La clase genérica `Respuesta<T>` estandariza las respuestas entre capas con `estado` (bool), `valor` (mensaje) y `objeto` (dato retornado).
- **MDI Shell (Single Panel)**: El formulario `Menu` carga los formularios hijos dentro de un `Panel`, simulando navegación MDI sin ventanas flotantes.

---

## 🗄️ Base de Datos

- **Nombre**: `SNet`
- **Motor**: SQL Server (LocalDB para desarrollo, SQL Server completo para producción AIEP)
- **Entidades principales**: `Cliente`, `Proveedor`, `Usuario`, `Region`, `Provincia`, `Comuna`
- Las operaciones se realizan exclusivamente mediante **Stored Procedures** (no se usa SQL en línea)

---

## 🚀 Cómo Ejecutar el Proyecto

1. **Requisitos previos:**
   - Visual Studio 2022 (o superior)
   - SQL Server LocalDB (incluido con Visual Studio) o SQL Server Express
   - .NET Framework instalado

2. **Configurar la base de datos:**
   - Crear la base de datos `SNet` en SQL Server
   - Ejecutar los scripts de creación de tablas y stored procedures correspondientes

3. **Configurar la cadena de conexión:**
   - Abrir `Datos/Conexion.cs`
   - Ajustar la cadena `Conex` según el entorno (LocalDB o SQL Server con autenticación)

4. **Compilar y ejecutar:**
   - Abrir `Aiep2025.sln` en Visual Studio
   - Establecer `Presentacion` como proyecto de inicio
   - Presionar `F5` o `Ctrl+F5`

---

## 📐 Flujo de Datos (Ejemplo: Ingresar un Cliente)

```
[PCli_Ing.cs]           ← Usuario llena el formulario y presiona "Guardar"
      ↓
[NCliente.Ingresar()]   ← Negocio valida y prepara el objeto ECliente
      ↓
[DCliente.Ingresar()]   ← Datos ejecuta el SP "Ing_Cliente" en SQL Server
      ↓
[Respuesta<bool>]       ← El resultado sube de vuelta hasta la vista
```

---

## ✅ Funcionalidades Implementadas

- [x] Autenticación de usuarios (Login)
- [x] CRUD completo de Clientes
- [x] CRUD completo de Proveedores
- [x] Consulta de Regiones, Provincias y Comunas
- [x] Validación y formateo de RUT chileno
- [x] Navegación por menú con formularios embebidos

## 🔲 Funcionalidades Pendientes

- [ ] CRUD de Productos
- [ ] Generación de arreglos / reportes
- [ ] CRUD de Usuarios desde la interfaz

---

## 👨‍💻 Contexto Académico

Proyecto desarrollado como parte del **Taller de Base de Datos 2026** en el instituto **AIEP**, aplicando conceptos de arquitectura en capas, acceso a datos con ADO.NET y desarrollo de interfaces con Windows Forms.
