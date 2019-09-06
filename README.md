# ProjectDTOs
Corresponde a una práctica del curso de "Construyendo Web Api RestFul" con ASP.NET Core 2.2 en Udemy por Felipe Gavilán.

## Información a tener en cuenta en la configuración
Fue desarrollado con Visual Studio Community 2017 Versión 15.7.3 y .Net Framework Versión 4.7.
Para el desarrollo de este pequeño proyecto es necesario descargar e instalar el SDK de .Net Core 2.2, la podemos descargar en el siguiente enlace: https://dotnet.microsoft.com/ allí buscamos la opción download y seleccionamos el SDK según nuestra versión del framework .Net que tengamos instalado.

### Contenido
El proyecto consta de un CRUD en el controlador `Autores`. En dónde podemos visualizar la construcción de las acciones Get, Post, Put, Patch y Delete.<br>
Recordemos que la acción Put es utilizada para la actualización completa del registro, por lo tanto debemos enviar todos los campos para que el `FromBody` los reciba y proceda.<br>
Por otro lado la acción Patch puede actualizar algunos campos, por lo tanto no es obligatorio enviar todos los campos, sólo los que se desea actualizar.<br>
Tiene 1 migración correspondientes a la creación de la BD. Se realizó con el localdb que brinda el Visual Studio, para no crear BD aparte.<br>

Con este proyecto se manejan dos modelos. Uno de ellos es para la conexión directa a la base de datos y el otro es para exponer los datos al cliente. Ejemplo: `Autor.cs` y `AutorDTO.cs`. De esta manera estamos protegiendo la información que puede ver o manipular el cliente. Éstos modelos fueron nombrados con la terminación DTO y son conocidos tambien como viewmodels.<br><br>

Fue necesario crear una Clase `SimpleMappings` para asociar los mapeos entre los modelos y los DTO. Posteriormente en el método `ConfigureServices` de la Clase `Starup` se configura el Auto Mapper creando una instancia de `MapperConfiguration` en donde agrega un `Profile` el cual en este caso corresponde a la clase `SimpleMappings`. <br>

Básicamente en este proyecto se aprende a manipular la información pasando datos de un DTO a un modelo y viceversa para exponer los datos de una manera segura.
