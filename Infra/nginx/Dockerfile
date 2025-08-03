# Usa la imagen oficial de Nginx
FROM nginx:alpine

# Copia el archivo de configuración personalizado
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia los archivos compilados de Angular al contenedor
COPY dist/FinanzautoFront/ /usr/share/nginx/html/

# Expone el puerto 80
EXPOSE 80
