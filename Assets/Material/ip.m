% Read the input image
inputImage = imread('cm.jpeg');

% Display the original image
figure;
subplot(1, 3, 1);
imshow(inputImage);
title('Original Image');

% Apply median filtering for noise reduction
filteredImage = medfilt2(inputImage, [3, 3]); % You can adjust the filter size

% Display the filtered image
subplot(1, 3, 2);
imshow(filteredImage);
title('Filtered Image');

% Perform image segmentation using Otsu's method
threshold = graythresh(filteredImage);
binaryImage = imbinarize(filteredImage, threshold);

% Display the segmented binary image
subplot(1, 3, 3);
imshow(binaryImage);
title('Segmented Image');

% Optional: You can also apply post-processing steps to improve segmentation results.

% Save the output segmented image
imwrite(binaryImage, 'output_segmented_image.jpg');
