### Gu�a para usar la imagen de Docker del Backend

#### Requisitos previos
1. Aseg�rate de tener Docker Desktop instalado en tu computadora. Puedes descargarlo [aqu�](https://www.docker.com/products/docker-desktop/).
2. Debes tener permisos de administrador en tu computadora para instalar Docker Desktop.

#### Pasos para usar la imagen de Docker

1. **Descarga e instala Docker Desktop:**
   - Si no lo tienes a�n ve al sitio web de Docker en [www.docker.com](https://www.docker.com/products/docker-desktop/).
   - Descarga la versi�n adecuada para tu sistema operativo (Windows, macOS o Linux).
   - Ejecuta el instalador descargado y sigue las instrucciones para completar la instalaci�n. Es posible que necesites reiniciar e ingresar tu contrase�a de administrador durante el proceso de instalaci�n, o otorgarle permisos de administrador al instalador.
   - Sigue solo la instalaci�n recomendada.

2. **Verifica que Docker Desktop est� en funcionamiento:**
   - Una vez instalado, abre Docker Desktop desde el men� de inicio o la barra de aplicaciones.
   - Espera a que Docker Desktop se inicie completamente. Ver�s un icono de Docker en tu barra de tareas o men� superior cuando est� listo.

3. **Descarga la imagen y los archivos del backend desde el repositorio:**
   - Descarga la carpeta desde [este enlace](https://drive.google.com/drive/folders/1RsVZXFIF8FDjAkbHYJ6UQXfb6qkwFbiu?usp=sharing).
   - Coloca la carpeta completa de DockerDeployment en tus 'Documentos' (La carpeta debe estar all� antes de continuar con los siguientes pasos)

4. **Configura la imagen y el certificado HTTPS:**
   - Si eres usuario de Windows, abre la carpeta y haz doble click en el certificado_https_windows, se abrir� una terminal que te dir� que se copio el certificado + la palabra clave del certificado en los directorios correspondientes, puedes cerrar esta terminal pulsando cualquier tecla.
   
   - Si eres usuario de Linux o macOS tienes que correr el Shell Script a trav�s de una terminal dentro de la carpeta de DockerDeployment, primero utiliza: `chmod +x certificado_https_linux_macOS` para permitir su ejecuci�n, y luego: `./certificado_https_linux_macOS` para ejecutarlo.
   
   - Ahora abre una terminal o l�nea de comandos (cmd) en la carpeta del proyecto, asegurate de que la terminal este en la direcci�n correcta, si usas Windows debes estar en C:\Users\TuNombreDeUsuario\Documentos\LuminaDeployment>, de lo contrario usa el siguiente comando para navegar a la carpeta dentro de la terminal:
     ```bash
     cd Documentos\LuminaDeployment
     ```
   - Luego ejecuta el siguiente comando para construir la imagen de Docker:
     ```bash
     docker load -i lumina-backend.tar
     ```
   - Y finalmente usa este �ltimo comando para que corra la imagen:
     ```bash
     docker-compose up
     ```
  
4. **Accede a la funcionalidad completa del website:**
   - Una vez que el contenedor est� en ejecuci�n, abre tu navegador web favorito.
   - Ingresa a la URL del website: https://loginnocountry.netlify.app

5. **Det�n el contenedor cuando hayas terminado:**
   - Cuando ya no necesites utilizar el backend, puedes detener el contenedor.
   - Ve a la ventana de terminal o l�nea de comandos donde ejecutaste el contenedor.
   - Presiona `Ctrl + C` para detener la ejecuci�n del contenedor.
   - Si deseas remover el contenedor para que no quede en segundo plano, ejecuta el siguiente comando:
     ```bash
     docker rm luminadeployment-Lumina-Backend-1
     ```

   - �Listo! Ahora puedes usar la imagen de Docker para ejecutar el backend de la aplicaci�n en tu PC siempre que desees abriendo Docker Desktop y usando el siguiente comando en la carpeta DockerDeployment (recuerda si usas Windows puedes usar: cd Documentos\LuminaDeployment si no estas en el directorio correcto en tu terminal):
     ```bash
     docker-compose up
     ```

   - Recuerda igualmente detener el contenedor con `Ctrl + C` y quitarlo del segundo plano usando:
     ```bash
     docker rm luminadeployment-Lumina-Backend-1
     ```