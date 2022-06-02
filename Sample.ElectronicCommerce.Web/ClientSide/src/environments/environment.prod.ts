export const environment = {
  version: "1.1.0206.3",
  production: true,  
  baseUrl: "http://localhost:9898",
  tokenWhitelistedDomains: [ 
    new RegExp('localhost:9898'), 
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Login'),
  ]
};
