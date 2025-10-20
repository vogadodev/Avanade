const singleSpaAngularWebpack = require('single-spa-angular/lib/webpack').default;

module.exports = (config, options) => {
    const singleSpaWebpackConfig = singleSpaAngularWebpack(config, options);
    // Pode adicionar configurações customizadas do Webpack aqui
    return singleSpaWebpackConfig;
};
