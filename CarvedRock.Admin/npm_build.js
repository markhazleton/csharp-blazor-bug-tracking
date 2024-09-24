const fs = require('fs');
const path = require('path');
const CleanCSS = require('clean-css');
const UglifyJS = require('uglify-js');
const sass = require('sass');

// Function to minify CSS
function minifyCSS() {
    const scssInputPath = path.join(__dirname, 'src/scss/');
    const cssOutputPath = path.join(__dirname, 'src/scss/');
    const distOutputPath = path.join(__dirname, 'wwwroot/dist/css/');
    const scssOutputFile = 'compiled_style.css';  // Temporary output file for compiled SCSS
    const minifiedOutputFile = 'style.min.css';

    // Compile SCSS to CSS
    const result = sass.compile(scssInputPath + 'main.scss', {
        outputStyle: 'expanded'  // Expanded for initial compilation, minification will handle compression
    });

    // Write the compiled CSS to a temporary file
    fs.writeFileSync(cssOutputPath + scssOutputFile, result.css);

    // Minify the compiled CSS along with other CSS files
    let output = new CleanCSS({}).minify([
        cssOutputPath + scssOutputFile,
        cssOutputPath + 'site.css'
    ]);

    // Write the final minified CSS to the distribution folder
    fs.writeFileSync(distOutputPath + minifiedOutputFile, output.styles);

    // Optionally, clean up the temporary file
    fs.unlinkSync(cssOutputPath + scssOutputFile);
}

function minifyJS() {
    const inputPath = path.join(__dirname, 'src/js/');
    const bootstrapJSPath = path.join(__dirname, 'node_modules/bootstrap/dist/js/bootstrap.bundle.js');
    const outputPath = path.join(__dirname, 'wwwroot/dist/js/');
    const outputFile = 'scripts.min.js';

    const files = {
        'bootstrap.js': fs.readFileSync(bootstrapJSPath, 'utf8'),
        'site.js': fs.readFileSync(inputPath + 'site.js', 'utf8')
    };

    const result = UglifyJS.minify(files);

    if (!result.error) {
        fs.writeFileSync(outputPath + outputFile, result.code);
    } else {
        console.error('Error minifying JS:', result.error);
    }
}

// Run the functions
minifyCSS();
minifyJS();
