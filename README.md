# 🚀 ACRP API

Este proyecto es una API para gestionar usuarios, secciones y páginas. Utiliza **ASP.NET Core**, **MongoDB** y **JWT** para la autenticación.

---

## 📋 Requisitos

- [.NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Visual Studio Code](https://code.visualstudio.com/) (opcional)

---

## ⚙️ Configuración

### Variables de entorno

Crea un archivo `.env` en la raíz del proyecto con las siguientes variables de entorno:

```plaintext
JWT_SECRET_KEY=your_jwt_secret_key
JWT_ISSUER=your_jwt_issuer
JWT_AUDIENCE=your_jwt_audience
JWT_EXPIRATION_IN_MINUTES=your_jwt_expiration_in_minutes
MONGODB_CONNECTION_STRING=your_mongodb_connection_string
MONGODB_DATABASE_NAME=your_mongodb_database_name
ALLOWED_ORIGINS=http://localhost:3000,http://localhost:5000
```

### Configuración de `appsettings.json`

Asegúrate de que `appsettings.json` esté configurado correctamente para la limitación de tasa (rate limiting):

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "IpRateLimiting": {
    "EnableEndpointRateLimiting": true,
    "StackBlockedRequests": false,
    "RealIpHeader": "X-Real-IP",
    "ClientIdHeader": "X-ClientId",
    "HttpStatusCode": 429,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 10
      },
      {
        "Endpoint": "POST:/api/user/register",
        "Period": "1m",
        "Limit": 2
      }
    ]
  }
}
```

---

## 🛠️ Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/VictorHerdz10/ACRP_API.git
   cd tu_repositorio
   ```

2. Restaura los paquetes NuGet:

   ```bash
   dotnet restore
   ```

3. Compila el proyecto:

   ```bash
   dotnet build
   ```

---

## 🚀 Ejecución

Para ejecutar la API, usa el siguiente comando:

```bash
dotnet run
```

La API estará disponible en [http://localhost:5000](http://localhost:5000).

---

## 🌐 Endpoints

### Usuarios

- **POST** `/api/user/register`: Registra un nuevo usuario.
- **POST** `/api/user/login`: Inicia sesión de un usuario.
- **DELETE** `/api/user/{id}`: Elimina un usuario por su ID.

### Secciones

- **GET** `/api/section/all`: Obtiene todas las secciones.
- **GET** `/api/section/{id}`: Obtiene una sección por su ID.
- **POST** `/api/section`: Crea una nueva sección.
- **PUT** `/api/section/{id}`: Actualiza una sección por su ID.
- **DELETE** `/api/section/{id}`: Elimina una sección por su ID.

### Páginas

- **GET** `/api/page/all`: Obtiene todas las páginas.
- **GET** `/api/page/{id}`: Obtiene una página por su ID.
- **POST** `/api/page`: Crea una nueva página.
- **PUT** `/api/page/{id}`: Actualiza una página por su ID.
- **DELETE** `/api/page/{id}`: Elimina una página por su ID.

---

## 🛡️ Middleware

### CustomAuthorizationMiddleware

Este middleware personalizado maneja las respuestas de autorización y autenticación, devolviendo un mensaje de error `401 Unauthorized` personalizado cuando un usuario no está autenticado.

---

## ⏱️ Rate Limiting

La API utiliza **AspNetCoreRateLimit** para limitar la tasa de solicitudes. La configuración se encuentra en `appsettings.json`.

---

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un **issue** o un **pull request** en [GitHub](https://github.com/tu_usuario/tu_repositorio).

---

## 📄 Licencia

Este proyecto está licenciado bajo la **Licencia MIT**. Consulta el archivo [LICENSE](LICENSE) para más detalles.
```

---

### **Características del README**
- **Iconos**: Usa emojis para mejorar la visualización.
- **Sintaxis correcta**: Formato Markdown válido para GitHub.
- **Secciones claras**: Requisitos, configuración, instalación, ejecución, endpoints, middleware, rate limiting, contribuciones y licencia.