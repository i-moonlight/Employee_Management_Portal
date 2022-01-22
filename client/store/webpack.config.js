const path = require('path');

module.exports = {
    mode: 'development',
    devtool: 'source-map',

    entry: './dist/main.js',
    output: {
        path: path.resolve(__dirname, 'out'),
        filename: 'main.js',
        //     entry: path.resolve(__dirname, 'src', 'index.js'),
        //     output: {
        //         path: path.resolve(__dirname, 'dist'),
        //         clear: true,
        //         filename: 'main.js',
    },

    // mode: 'development',
    // devtool: 'source-map',
    // entry: path.join(__dirname, 'src', 'main', 'resources', 'static', 'js', 'main.js'),
    // devServer: {
    //     contentBase: './dist',
    //     compress: true,
    //     port: 3000,
    //     allowedHosts: [
    //         'localhost:9000'
    //     ]
    // },
    // module: {
    //     rules: [
    //         {
    //             test: /\.js$/,
    //             exclude: /(node_modules|bower_components)/,
    //             use: {
    //                 loader: 'babel-loader',
    //                 options: {
    //                     presets: ['@babel/preset-env']
    //                 }
    //             }
    //         }
    //     ]
    // },

    resolve: {
        modules: [
            path.join(__dirname, 'src', 'main', 'resources', 'static', 'js'),
            path.join(__dirname, 'node_modules'),
        ],
    }

}
