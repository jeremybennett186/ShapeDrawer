import { TestBed } from '@angular/core/testing';

import { CommandToShapeParserService } from './command-to-shape-parser.service';

describe('CommandToShapeParserService', () => {
  let service: CommandToShapeParserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CommandToShapeParserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
