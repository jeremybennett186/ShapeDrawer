import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Polygon } from './models/polygon';
import { Oval } from './models/oval';
import { CommandToShapeParserService } from './services/command-to-shape-parser.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  @ViewChild('shapeCanvas', {static: false}) canvas: ElementRef<HTMLCanvasElement>;
  context: CanvasRenderingContext2D;
  title = 'shape-drawer';
  command: string = ""
  errorMessage: string = ""

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
        }
      },
      e => {
        this.errorMessage = e.error.message;
      })
  }

  constructor (private commandToShapeParserService: CommandToShapeParserService) {  }

  ngOnInit() {  }

  ngAfterViewInit() {
    const res = this.canvas.nativeElement.getContext('2d');
    if (!res || !(res instanceof CanvasRenderingContext2D)) {
        throw new Error('Failed to get 2D context');
    }
    this.context = res;
  }

  drawPolygon(polygon: Polygon) {
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
    this.context.canvas.height = oval.height;
    this.context.canvas.width = oval.width;

    this.context.clearRect(0, 0, oval.width, oval.height);
    this.context.fillStyle = this.getRandomColour();
    this.context.beginPath();
    this.context.ellipse(oval.width/2, oval.height/2, oval.width/2, oval.height/2, 0, 0, 2 * Math.PI);
    this.context.fill();
  }

  getRandomColour() {
    var letters = '0123456789ABCDEF';
    var colour = '#';
    for (var i = 0; i < 6; i++) {
      colour += letters[Math.floor(Math.random() * 16)];
    }
    return colour;
  }
}
