import express, {Request, Response} from "express";
import dotenv from 'dotenv';
import taskRoute from './routes/task.route'
import groupRoute from './routes/group.task'
import personRoute from "./routes/person.task";
import swaggerUi from 'swagger-ui-express';
import specs from '../swagger';

dotenv.config();
const app = express();

const PORT = process.env.PORT || 3000;

app.use("/api/-docs", swaggerUi.serve, swaggerUi.setup(specs));
app.use("/Task", taskRoute);
app.use("/Group", groupRoute);
app.use("/Group", personRoute);

app.listen(PORT, ()=>{
    console.log(`Servidor corriendo en el puerto ${PORT}`);
});
