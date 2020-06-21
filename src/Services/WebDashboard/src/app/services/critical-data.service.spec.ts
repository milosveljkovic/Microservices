/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CriticalDataService } from './critical-data.service';

describe('Service: CriticalData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CriticalDataService]
    });
  });

  it('should ...', inject([CriticalDataService], (service: CriticalDataService) => {
    expect(service).toBeTruthy();
  }));
});
