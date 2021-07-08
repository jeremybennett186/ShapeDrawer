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
  polygon: Polygon = {
    type: "polygon",
    coordinates: [
      { x: 0, y: 0 },
      { x: 100, y:100 },
      { x: 100, y:0 },
      { x: 0, y: 0 }
    ]
  }
  oval: Oval = {
    type: "oval",
    height: 75,
    width: 25
  }
  canvasWidth: number = 500
  canvasHeight: number = 500
  command: string = ""


  draw() {
    this.commandToShapeParserService.getShape(this.command)
    // resp is of type `HttpResponse<Config>`
    .subscribe(resp => {
      // access the body directly, which is typed as `Config`.

      var response = { ...resp! };

      if (response.type === "Polygon") {
        this.drawPolygon(response)
      } else if (response.type === "Oval") {
        this.drawOval(response)
      }
    });

    console.log(this.polygon)
  }

  constructor (private commandToShapeParserService: CommandToShapeParserService) {

  }

  ngOnInit() {

  }

  ngAfterViewInit() {
    const res = this.canvas.nativeElement.getContext('2d');
    if (!res || !(res instanceof CanvasRenderingContext2D)) {
        throw new Error('Failed to get 2D context');
    }
    this.context = res;

    //this.drawPolygon();

    //this.drawOval();


  }

  drawPolygon(polygon: Polygon) {
    const res = this.canvas.nativeElement.getContext('2d');
    if (!res || !(res instanceof CanvasRenderingContext2D)) {
        throw new Error('Failed to get 2D context');
    }
    this.context = res;
    console.log('hello')
    var maxX = Math.max.apply(null, polygon.coordinates.map(function(coordinate) {
      return coordinate.x
    }))

    var maxY = Math.max.apply(null, polygon.coordinates.map(function(coordinate) {
      return coordinate.y
    }))




    //this.canvasHeight = maxX
    //this.canvasWidth = maxY

    console.log(this.context)

    this.context.clearRect(0, 0, this.canvasWidth, this.canvasHeight)
    this.context.fillStyle = '#f00';
    this.context.beginPath();
    console.log('start drawing')
    polygon.coordinates.forEach((coordinate, index) => {
      
      if (index === 0) {
        this.context.moveTo(coordinate.x, coordinate.y)
      } else {
        console.log(coordinate.x)
        console.log(coordinate.y)
        this.context.lineTo(coordinate.x, coordinate.y)
      }
    })
    console.log('end drawing')
    this.context.closePath();
    this.context.fill();


  }

  drawOval(oval: Oval) {
    //this.canvasHeight = oval.height
    //this.canvasWidth = oval.width
    this.context.clearRect(0, 0, this.canvasWidth, this.canvasHeight)
    this.context.fillStyle = '#0000ff';
    this.context.beginPath()
    this.context.ellipse(oval.width, oval.height, oval.width, oval.height, 0, 0, 2 * Math.PI);
    this.context.fill()
  }
}
