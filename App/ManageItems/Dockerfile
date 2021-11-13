FROM node:14-alpine as node
WORKDIR /GetItemsDetailsByCode
COPY ["App/ManageItems/", "App/ManageItems/"]

COPY . .
WORKDIR "/GetItemsDetailsByCode/App/ManageItems"
RUN npm install
RUN npm run build --prods
#stage 2
FROM nginx:alpine
COPY --from=node /GetItemsDetailsByCode/App/ManageItems/dist/ManageItems /usr/share/nginx/html