import swaggerJsdoc from 'swagger-jsdoc';
import path from 'path';

const options: swaggerJsdoc.Options = {
  swaggerDefinition: {
    openapi: '3.0.0',
    info: {
      title: 'API Documentation',
      version: '1.0.0',
      description: 'Documentación de la API para Express con Swagger',
    },
  },
  apis: [path.resolve("src/routes/*.ts", './routes/*.ts')], // Especifica la ubicación de tus archivos de rutas
};

const specs = swaggerJsdoc(options);

export default specs;