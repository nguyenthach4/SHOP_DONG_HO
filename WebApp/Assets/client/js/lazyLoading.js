function load(img) {
	const url = img.getAttribute('lazy-src')

	img.setAttribute('src', url)
	img.removeAttribute('lazy-src');

}
function ready() {
	if ('IntersectionObserver' in window) {
		var lazImage = document.querySelectorAll('[lazy-src]');	
		let obServer = new IntersectionObserver((entries) => {
			entries.forEach(enity => {
				if (enity.isIntersecting) {
					load(enity.target);
					obServer.unobserve(enity.target)
				}
				
			})
			
		});
		lazImage.forEach(img => {
			obServer.observe(img)
			
		})
		
	}
	else {

	}
}
document.addEventListener('DOMContentLoaded', ready)