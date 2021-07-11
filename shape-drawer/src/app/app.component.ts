import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { CommandToShapeParserService } from './services/command-to-shape-parser.service';
import { Polygon } from './models/polygon';
import { Oval } from './models/oval';
import { Prism } from './models/prism';
import { Coordinate } from './models/coordinate';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  @ViewChild('shapeCanvas', {static: true}) canvas: ElementRef<HTMLCanvasElement>;
  context: CanvasRenderingContext2D;
  title = 'shape-drawer';
  command: string = "";
  errorMessage: string = "";

  drawShape() {
    this.errorMessage = "";
    this.context.clearRect(0, 0, this.context.canvas.width, this.context.canvas.height);

    this.commandToShapeParserService.getShape(this.command)
      .subscribe(resp => {
        var response = { ...resp! };
        if (response.type === "Polygon") {
          this.drawPolygon(response);
        } else if (response.type === "Oval") {
          this.drawOval(response);
        } else if (response.type === "Prism") {
          this.drawPrism(response);
        }
      },
      e => {
        if (!e.error.message) {
          this.errorMessage = "Unable to call shape parser service";
        } else {
          this.errorMessage = e.error.message;
        }
      })
  }

  constructor (private commandToShapeParserService: CommandToShapeParserService) {
  }

  ngOnInit() {  }

  ngAfterViewInit() {
    // intialize context
    const context = this.canvas.nativeElement.getContext('2d');
    if (!context || !(context instanceof CanvasRenderingContext2D)) {
        throw new Error('Failed to get 2D context');
    }
    this.context = context;
  }

  drawPolygon(polygon: Polygon) {
    // resize canvas
    var maxX = Math.max.apply(null, polygon.coordinates.map(function(coordinate) {
      return coordinate.x;
    }))

    var maxY = Math.max.apply(null, polygon.coordinates.map(function(coordinate) {
      return coordinate.y;
    }))

    this.context.canvas.height = maxY;
    this.context.canvas.width = maxX;

    this.context.clearRect(0, 0, maxX, maxY);
    this.context.fillStyle = this.getRandomColour();
    this.context.beginPath();

    // draw shape
    polygon.coordinates.forEach((coordinate, index) => {
      if (index === 0) {
        this.context.moveTo(coordinate.x, coordinate.y);
      } else {
        this.context.lineTo(coordinate.x, coordinate.y);
      }
    })

    this.context.closePath();
    this.context.fill();
  }

  drawOval(oval: Oval) {
    // resize canvas
    this.context.canvas.height = oval.height;
    this.context.canvas.width = oval.width;

    // draw shape
    this.context.fillStyle = this.getRandomColour();
    this.context.beginPath();
    this.context.ellipse(oval.width/2, oval.height/2, oval.width/2, oval.height/2, 0, 0, 2 * Math.PI);
    this.context.fill();
  }

  drawPrism(prism: Prism) {
    // get centre position
    const centre: Coordinate = ({
      x: prism.width,
      y: (prism.height + (prism.width + prism.length) / 2)
    });

    // resize canvas
    this.context.canvas.height = centre.y;
    this.context.canvas.width = prism.width + prism.length;

    var colour = this.getRandomColour()

    // draw left face
    this.context.beginPath();
    this.context.moveTo(centre.x, centre.y);
    this.context.lineTo(centre.x - prism.width, centre.y - prism.width * 0.5);
    this.context.lineTo(centre.x - prism.width, centre.y - prism.height - prism.width * 0.5);
    this.context.lineTo(centre.x, centre.y - prism.height * 1);
    this.context.closePath();
    this.context.fillStyle = this.shadeColour(colour, -10);
    this.context.strokeStyle = colour;
    this.context.stroke();
    this.context.fill();
  
    // draw right face
    this.context.beginPath();
    this.context.moveTo(centre.x, centre.y);
    this.context.lineTo(centre.x + prism.length, centre.y - prism.length * 0.5);
    this.context.lineTo(centre.x + prism.length, centre.y - prism.height - prism.length * 0.5);
    this.context.lineTo(centre.x, centre.y - prism.height * 1);
    this.context.closePath();
    this.context.fillStyle = this.shadeColour(colour, 10);
    this.context.strokeStyle = this.shadeColour(colour, 50);
    this.context.stroke();
    this.context.fill();
  
    // draw top face
    this.context.beginPath();
    this.context.moveTo(centre.x, centre.y - prism.height);
    this.context.lineTo(centre.x - prism.width, centre.y - prism.height - prism.width * 0.5);
    this.context.lineTo(centre.x - prism.width + prism.length, centre.y - prism.height - (prism.width * 0.5 + prism.length * 0.5));
    this.context.lineTo(centre.x + prism.length, centre.y - prism.height - prism.length * 0.5);
    this.context.closePath();
    this.context.fillStyle = this.shadeColour(colour, 20);
    this.context.strokeStyle = this.shadeColour(colour, 60);
    this.context.stroke();
    this.context.fill();
  }

  // darken or lighten a colour by a percentage
  shadeColour(colour: string, percent: number) {
    colour = colour.substr(1);
    const number = parseInt(colour, 16),
      amt = Math.round(2.55 * percent),
      R = (number >> 16) + amt,
      G = (number >> 8 & 0x00FF) + amt,
      B = (number & 0x0000FF) + amt;
    return '#' + (0x1000000 + (R < 255 ? R < 1 ? 0 : R : 255) * 0x10000 + (G < 255 ? G < 1 ? 0 : G : 255) * 0x100 + (B < 255 ? B < 1 ? 0 : B : 255)).toString(16).slice(1);
  }

  // generate a random colour
  getRandomColour() {
    const letters = '0123456789ABCDEF';
    var colour = '#';
    for (let i = 0; i < 6; i++) {
      colour += letters[Math.floor(Math.random() * 16)];
    }
    return colour;
  }
}
