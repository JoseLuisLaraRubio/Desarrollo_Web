import { TestBed } from "@angular/core/testing";

import { ErrorMessageProviderService } from "./error-message-provider.service";

describe("ErrorMessageProviderService", () => {
  let service: ErrorMessageProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ErrorMessageProviderService);
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });
});
