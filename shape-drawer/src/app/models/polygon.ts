import { Coordinate } from "./coordinate";
import { Shape } from "./shape"

export interface Polygon extends Shape {
    coordinates: Coordinate[]
}