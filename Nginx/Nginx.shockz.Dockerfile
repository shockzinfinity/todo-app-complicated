FROM nginx:latest

COPY nginx.conf /etc/nginx/nginx.conf
COPY fullchain.pem /etc/ssl/certs/fullchain.pem
COPY privkey.pem /etc/ssl/private/privkey.pem
COPY chain.pem /etc/ssl/certs/chain.pem
COPY options-ssl-nginx.conf /etc/nginx/conf.d/options-ssl-nginx.conf
