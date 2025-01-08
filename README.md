# Prueba3BackEndV2.0

1.- En una carpeta creada abrir el cmd, luego en la cmd se coloca el siguiente comando:
``` 
git clone <url del repositorio>
```

2.- Abrir el proyecto clonado y dentro de este abrir la terminal y colocar:
```
dotnet restore
```

3.- En la raiz del proyecto crear un archivo con el nombre ".env" y dentro de esta colocar la siguiente linea:
```
DATABASE_URL=Data Source=database.db
JWT_KEY=EsteEsUnClaveSecretaMuySeguraYCon32CaracteresSAFASFASFASFASFASFASFA123F31
JWT_ISSUER=ApiPrueba3
JWT_AUDIENCE=ApiPrueba3Users

CLOUDINARY_CLOUD_NAME=drmaozzpw
CLOUDINARY_API_KEY=788936899115272
CLOUDINARY_API_SECRET=ivobf4ib4NGY_TmhW9ns5Fmi2C0
```

4.- Finalmente para iniciar y correr el proyecto se coloca en la terminal el siguiente comando:
```
Dotnet run
```