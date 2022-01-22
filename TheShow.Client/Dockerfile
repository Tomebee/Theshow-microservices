
# Name the node stage "builder"
FROM node:10 AS builder

ENV EXTEND_ESLINT true
ENV REACT_APP_API_BASE_URL http://kubernetes.local/core
ENV REACT_APP_AUTHORITY_CLIENT_ID reactapp
ENV REACT_APP_AUTHORITY_CLIENT_SECRET 49C1A7E1-0C79-4A89-A3D6-A37998FB86B0
ENV REACT_APP_AUTHORITY_URL http://identity.local
# Set working directory
WORKDIR /app
# Copy all files from current directory to working dir in image
COPY . .
# install node modules and build assets
RUN yarn install && yarn build

FROM nginx:alpine
COPY --from=builder /app/build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]