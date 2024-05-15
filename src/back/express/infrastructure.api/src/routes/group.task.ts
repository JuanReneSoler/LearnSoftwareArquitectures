import express, { Request, Response } from 'express';
import {find, list, add, errace, update} from '../controllers/group.controller'

const groupRoute = express.Router();

groupRoute.get('/', list);
groupRoute.get('/:id', find);

export default groupRoute;