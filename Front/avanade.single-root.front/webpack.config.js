const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  entry: './src/avanade-root-config.js',
  output: {
    filename: 'avanade-root-config.js',
    path: path.resolve(__dirname, 'dist'),
    libraryTarget: 'system',
  },
  module: {
    rules: [
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
    ],
  },
  devServer: {
    historyApiFallback: true,
    port: 9000,
    headers: {
      'Access-Control-Allow-Origin': '*',
    },

    allowedHosts: ['avanade.ecommerce.com.br', 'localhost'],
    host: '0.0.0.0',
    inline: false, // Desativa o cliente Live Reload
    hot: false, // Desativa o cliente Live Reload
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: 'src/index.ejs',
    }),
    new CopyWebpackPlugin({
      patterns: [{ from: 'src/style.css', to: '' }],
    }),
  ],
  externals: ['single-spa', 'systemjs', 'single-spa-layout'],
};
