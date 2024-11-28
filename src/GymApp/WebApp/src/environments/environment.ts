// Ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`
import "zone.js/plugins/zone-error";

export const environment = {
  production: false,
  passwordMinLength: 5,
} as const;
