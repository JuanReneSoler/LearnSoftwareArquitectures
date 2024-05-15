import express, { Request, Response } from 'express';
import {find, list, add, errace, update} from '../controllers/person.controller'

const personRoute = express.Router();

personRoute.get('/', list);
personRoute.get('/:id', find);

export default personRoute;