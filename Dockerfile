# Stage 1: Build the React app
FROM node:20 AS react-build
WORKDIR /app
COPY ./reactmahjong/package.json package.json
COPY ./reactmahjong/package-lock.json package-lock.json
RUN npm install
COPY ./reactmahjong .
RUN npm run build

FROM nginx:alpine
COPY --from=react-build /app/default.conf.template /etc/nginx/templates/default.conf.template
COPY --from=react-build /app/dist /usr/share/nginx/html
# Expose the default nginx port
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

# Stage 2: Build the .NET Aspire app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AspireSample.sln ./
COPY ["AspireSample.AppHost/AspireSample.AppHost.csproj", "AspireSample.AppHost/"]
COPY ["AspireSample.ApiService/AspireSample.ApiService.csproj", "AspireSample.ApiService/"]
COPY ["AspireSample.Mahjong/AspireSample.Mahjong.csproj", "AspireSample.Mahjong/"]
COPY ["AspireSample.ServiceDefaults/AspireSample.ServiceDefaults.csproj", "AspireSample.ServiceDefaults/"]
COPY ["AspireSample.Tests/AspireSample.Tests.csproj", "AspireSample.Tests/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
COPY ["nugets/", "nugets/"]
RUN dotnet workload restore "AspireSample.AppHost/AspireSample.AppHost.csproj"
#RUN dotnet restore
COPY . .
#WORKDIR "/src/AspireSample.AppHost"
#RUN dotnet build "AspireSample.AppHost.csproj" -c Release -o /src/build
WORKDIR "/src/"
RUN dotnet build "AspireSample.sln" -c Release -o /src/build

# Stage 3: Publish the .NET Aspire app
FROM build AS publish
#RUN dotnet publish "AspireSample.AppHost.csproj" -c Release -o /src/publish
RUN dotnet publish "AspireSample.sln" -c Release -o /src/publish

# Stage 4: Final stage - Combine React and .NET Aspire
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /src
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "AspireSample.AppHost.dll"]
