### Gu�a para usar la imagen de Docker del Backend

#### Requisitos previos
1. Aseg�rate de tener Docker Desktop instalado en tu computadora. Puedes descargarlo [aqu�](https://www.docker.com/products/docker-desktop/).
2. Debes tener permisos de administrador en tu computadora para instalar Docker Desktop.

#### Pasos para usar la imagen de Docker

1. **Descarga e instala Docker Desktop:**
   - Si no lo tienes a�n ve al sitio web de Docker en [www.docker.com](https://www.docker.com/products/docker-desktop/).
   - Descarga la versi�n adecuada para tu sistema operativo (Windows, macOS o Linux).
   - Ejecuta el instalador descargado y sigue las instrucciones para completar la instalaci�n. Es posible que necesites reiniciar y ingresar tu contrase�a de administrador durante el proceso de instalaci�n o otorgarle permisos de administrador al instalador.
   - Sigue la instalaci�n recomendada.

2. **Verifica que Docker Desktop est� en funcionamiento:**
   - Una vez instalado, abre Docker Desktop desde el men� de inicio o la barra de aplicaciones.
   - Espera a que Docker Desktop se inicie completamente. Ver�s un icono de Docker en tu barra de tareas o men� superior cuando est� listo.

3. **Descarga la imagen del backend desde el repositorio:**
   - Descargala desde [este enlace](https://github.com/No-Country/c17-116-n-csharp/tree/backend-development/Docker-Deployment).
   - Abre una ventana de terminal o l�nea de comandos en tu computadora.
   - Navega hasta el directorio donde descargaste la imagen de Docker.
   - Ejecuta el siguiente comando para ejecutar la imagen de Docker y crear un contenedor:
     ```bash
     docker run -p 7024:7024 lumina-backend
     ```
  
4. **Accede al backend desde tu navegador web:**
   - Una vez que el contenedor est� en ejecuci�n, abre tu navegador web favorito.
   - Ingresa a la URL del website: https://loginnocountry.netlify.app/login

5. **Det�n el contenedor cuando hayas terminado:**
   - Cuando ya no necesites utilizar el backend, puedes detener el contenedor.
   - Ve a la ventana de terminal o l�nea de comandos donde ejecutaste el contenedor.
   - Presiona `Ctrl + C` para detener la ejecuci�n del contenedor.
   - Si deseas remover el contenedor para que no quede en segundo plano, ejecuta el siguiente comando:
     ```bash
     docker rm lumina-backend
     ```

�Listo! Ahora puedes usar la imagen de Docker para ejecutar el backend de la aplicaci�n en tu PC siempre que desees usando:
     ```bash
     docker run -p 7024:7024 lumina-backend
     ```

   - Recuerda cerrar igualmente la terminal con Ctrl + C y:
     ```bash
     docker rm lumina-backend
     ```