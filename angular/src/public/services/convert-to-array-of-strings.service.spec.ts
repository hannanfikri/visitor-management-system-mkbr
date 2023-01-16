import { TestBed } from '@angular/core/testing';

import { ConvertToArrayOfStringsService } from './convert-to-array-of-strings.service';

describe('ConvertToArrayOfStringsService', () => {
  let service: ConvertToArrayOfStringsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConvertToArrayOfStringsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
