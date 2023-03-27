FROM node:18.15-alpine as build-env
WORKDIR /app

COPY . .
RUN npm install
RUN npm ci && npm run build --omit=dev

#stage 2
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build-env /app/dist/app /usr/share/nginx/html


