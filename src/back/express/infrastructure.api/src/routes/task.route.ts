import express, { Request, Response } from 'express';
import {find, list, add, errace, update} from '../controllers/task.controller'

const taskRoute = express.Router();

taskRoute.get('/', list);
taskRoute.get('/:id', find);

export default taskRoute;