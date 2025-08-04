# Contenerización (Docker)
-docker-compose.yml funcional. = [en la raiz del proyecto](https://github.com/orojasli1991/Finanzauto/blob/main/docker-compose.yml)
-Archivos Dockerfile para cada componente = https://github.com/orojasli1991/Finanzauto/blob/main/Finanzauto.ProductsApi/Dockerfile
# Documentación de cómo levantar el entorno y probarlo localmente.
  1. Clonar el repositorio = git clone https://github.com/orojasli1991/Finanzauto.git cd Finanzauto
  2. Compila y levanta los servicios (backend, base de datos y frontend) = docker-compose up --build
  3. Accede a la aplicación:
     - Frontend SPA: http://localhost:8080/login
     - Swagger UI (API): http://localhost:5000/swagger/index.html
     - Nginx = http://localhost:8080/
  4.Pruebas
     - Las pruebas unitarias e integradas están ubicadas en la carpeta tests/ = dotnet test
# Breve justificación de decisiones técnicas.
  - Backend
    - Net 8.0
    - Arquitectura por capas aplicando principios SOLID, teniendo bajo acoplamiento, con responsabilidades separadas Domain, Application, Infraestructura, API
    - Patrones de diseño como DTO para controlar los datos que se quieren exponer y Repository oara desacoplar la logica de accedo a datos del resto de la aplicacion
    - Seguridad por medio de jwt para endpoint considerados criticos
    - Manejo de errores por medio de middleware el cual captura una excecion en cualquier capa de la aplicacion y entrega una respuesta del error personalizada sin exponer datos sencibles
    - Cache en memoria para endpoint considerados estaticos que no cambian mucho en el tiempo y evita consultas innecesarias a la db
    - Pruebas unitarias y de integracion
    - Archivo docker
    - Entidades con validacion en los atributos por medio de decorados
    - Uso de EF
      
  - FrontEnd
    - Angular 20.1
    - Spa componentes standalone = sencilla para consumir el api
    - AuthGuard para manejo de rutas protegidas por medio de logueo
    - Interceptor en cada request, para enviar el token JWT automáticamente en cada request HTTP al backend
    - Validaciones de campos del formulario
  
  - DB
    - Sql server
      
# Estrategia de Testing
    -  xUnit / Microsoft.AspNetCore.Mvc.Testing / 
    -  Evidencia de ejecucion de pruebas = tests/Finanzauto.Products.Tests.Unit/Print/tests unitario e integracion.png
    
# Explique cómo las integraría en un pipeline CI/CD y qué métricas de calidad usaría para validar cumplimiento (ej.: cobertura, tiempos, errores críticos).
  1. En un pipeline de herramientas como azure devoos, GItHub actions etc.
  2. En el build verificar que todas las prueba pasen y aplicar condiciones en el pipeline si fallan pruebas criticas o un pocentaje previamente definido no complete el build
  3. En el deploy solo continua si las pruebas fueron exitosas
  4. metircas de calidad
     - Cobertura de pruebas normalmente esta entre 80 o 85 porciento del codigo.
     - Analisis de codigo estatico como sonarqube para detectar code smells o malas practicas de desarrollo
     - Tiempo de ejecucion identificaria cuellos de botella
     - Errores criticos o excepciones no controladas, realizando monitoreo de los test para identificar fallos por errores de logica, de acceso a datos o validaciones incorrectas

# Liderazgo técnico
  1. Para el tema del backend por lecciones aprendidas en diferentes proyectos donde me he enfrentado a esta situacion tomaria la desicion de incluir pruebas unitarias basicas que validen
      el core, servicios etc.. y asignar un responsable que este enfocado en las pruebas criticas con mi apoyo, a esto sumado de explicarle al equipo la importancia de esta deuda tecnica
  2. Validar contratos de datos usando DTO, interfaces e integrar pruebas de integracion con el frontend - backend, para evitar esto en un futuro, por lecciones aprendidas es clave la comunicacion previa entre el equipo
      front y back en la cual se alinean modelos y estruturas de los servicios, esto acompañado de un seguimiento de un uno a uno tecnico con una frecuencia cada 3 dias o por semana en el cual se
     valida tecnicamente como va el desarrollo
  3. Para el tema de los cambios sin control en la base de datos por lecciones aprendidas es bueno limitar los accesos de escitura en produccion, y tener disponible db de pruebas y desarrollo, adicional
     que en los pull request si se altera la db dicho cambio debe venir con el script de rollback para en caso de error poder reaccionar rapido.
   
      
       
